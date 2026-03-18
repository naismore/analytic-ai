import { useState, useEffect, useRef } from 'react';
import { ChatHeader } from '../../features/chatHeader';
import { ChatOverview } from '../../features/chatOverview';
import { ChatInput } from '../../features/chatInput';
import { useChatStore } from '../../store/chat';
import { useAuthStore } from '../../store/auth';
import { QuestionnaireModule } from '../questionnaireModule';
import {
  recommendationService,
  type CreateRecSessionRequest,
} from '../../services/recommendationService';

import { renderUserAnswers } from '../../utils/userAnswersMapper';

export const ChatModule = () => {
  const [isLoading, setIsLoading] = useState(false);
  const sentRef = useRef(false);

  const { chats, activeChatId, updateChat } = useChatStore();
  const { userId } = useAuthStore();

  const chat = chats.find((c) => c.chatId === activeChatId) ?? null;
  const questionnaire = chat?.questionnaire ?? { currentQuestion: 0, answers: {}, finished: true };
  const { finished, answers } = questionnaire;

  useEffect(() => {
    sentRef.current = false;
  }, [activeChatId]);

  const loadedMessagesRef = useRef<Record<string, boolean>>({});

  const createBotMessage = (
    results: { toolName: string; reasoningSummary: string; resultType: number }[],
    answersBlock: Record<string, any> | null,
    sessionId: string
  ) => {
    const userInfoBlock = answersBlock ? renderUserAnswers(answersBlock) : '';
    const recommendationsBlock = results
      .map(
        (r) => `<div class="p-4 mb-2 rounded-xl border border-gray-700 bg-gray-800">
          <p class="font-semibold text-lg mb-1">${
            r.resultType === 0 ? 'Основная рекомендация' : 'Альтернатива'
          }: ${r.toolName}</p>
          <p class="text-gray-300">${r.reasoningSummary}</p>
        </div>`
      )
      .join('');

    return {
      id: 1,
      author: 'bot' as const,
      text: `${userInfoBlock}${recommendationsBlock}`,
      time: new Date().toISOString(),
      isHtml: true,
      sessionId
    };
  };

  // ==================== LOAD OLD CHAT ====================
  useEffect(() => {
    const loadSessionMessages = async () => {
      if (!chat || chat.messages.length > 0) return;
      if (loadedMessagesRef.current[chat.chatId]) return;

      loadedMessagesRef.current[chat.chatId] = true;
      setIsLoading(true);

      try {
        const session = await recommendationService.getSession(chat.chatId);
        const answersBlock: Record<string, any> = {
          skillLevel: session.skillLevel,
          dataVolume: session.dataVolume,
          budget: session.budget,
          teamSize: session.teamSize,
          technicalBackground: session.technicalBackground,
          userTasks: session.userTasks,
          integrations: session.integrations
        };
        const botMessage = createBotMessage(session.results, answersBlock, session.sessionId);
        updateChat({ ...chat, messages: [botMessage] });
      } catch (err) {
        console.error('Ошибка при подгрузке сообщений чата:', err);
      } finally {
        setIsLoading(false);
      }
    };

    loadSessionMessages();
  }, [chat?.chatId, updateChat]);

  // ==================== CREATE NEW SESSION ====================
  useEffect(() => {
    const fetchRecommendations = async () => {
      if (!chat || !userId || sentRef.current || !finished) return;
      if (chat.sessionCreated) return;

      const hasSessionResult = chat.messages.some((m) => m.author === 'bot' && m.sessionId);
      if (hasSessionResult) return;

      sentRef.current = true;
      setIsLoading(true);

      try {
        const payload: CreateRecSessionRequest = {
          userId,
          skillLevel: answers['skillLevel'] as number,
          dataVolume: answers['dataVolume'] as number,
          userTasks: answers['userTasks'] as number[] || [],
          budget: answers['budget'] as number,
          teamSize: answers['teamSize'] as number || 0,
          technicalBackground: answers['technicalBackground'] as number,
          integrations: answers['integrations'] as number[] || []
        };

        const results = await recommendationService.createSession(payload);
        const botMessage = createBotMessage(results.results, answers, results.sessionId);

        updateChat({
          ...chat,
          title: `Сессия ${new Date().toLocaleDateString('ru-RU')}`,
          messages: [botMessage],
          sessionCreated: true
        });
      } catch (err) {
        console.error('Ошибка при создании рекомендаций:', err);
      } finally {
        setIsLoading(false);
      }
    };

    fetchRecommendations();
  }, [chat, finished, userId, answers, updateChat]);

  if (!chat) {
    return (
      <main className="flex-1 flex items-center justify-center">
        Выберите чат
      </main>
    );
  }

  return (
    <main className="flex-1 flex items-center justify-center">
      <div className="w-full max-w-7xl p-6">
        <div className="bg-[#0b0f1b] rounded-3xl border border-[#2b2f3b] overflow-hidden">
          <ChatHeader title={chat.title} />

          {!finished && <QuestionnaireModule />}

          {finished && <ChatOverview messages={chat.messages} isLoading={isLoading} />}

          <ChatInput messages={chat.messages} />
        </div>
      </div>
    </main>
  );
};
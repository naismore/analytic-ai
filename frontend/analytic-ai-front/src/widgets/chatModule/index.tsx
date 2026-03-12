import { useState, useEffect, useRef } from 'react'
import { ChatHeader } from '../../features/chatHeader'
import { ChatOverview } from '../../features/chatOverview'
import { ChatInput } from '../../features/chatInput'
import { useChatStore } from '../../store/chat'
import { useAuthStore } from '../../store/auth'
import { QuestionnaireModule } from '../questionnaireModule'
import { recommendationService, type CreateRecSessionRequest, type RecommendationResult } from '../../services/recommendationService'

export const ChatModule = () => {
  const [input, setInput] = useState('')
  const [isLoading, setIsLoading] = useState(false)
  const sentRef = useRef(false)

  const { chats, activeChatId, updateChat } = useChatStore()
  const { userId } = useAuthStore()

  const chat = chats.find((c) => c.chatId === activeChatId) ?? null
  const questionnaire = chat?.questionnaire ?? { currentQuestion: 0, answers: {}, finished: true }
  const { finished, answers } = questionnaire

  const sendMessage = () => {
    if (!input.trim() || !chat) return

    updateChat({
      ...chat,
      messages: [
        ...chat.messages,
        {
          id: chat.messages.length + 1,
          author: 'user',
          text: input,
          time: new Date().toISOString()
        }
      ]
    })

    setInput('')
  }

  // Сброс флага при смене чата
  useEffect(() => {
    sentRef.current = false
  }, [activeChatId])

  useEffect(() => {
    const fetchRecommendations = async () => {
      if (!chat || !userId || sentRef.current) return
      if (!finished) return

      // Если уже есть ответы бота (кроме первого приветственного), не отправляем повторно
      if (chat.messages.length > 1) {
        sentRef.current = true
        return
      }

      sentRef.current = true
      setIsLoading(true)

      try {
        const payload: CreateRecSessionRequest = {
          userId,
          skillLevel: answers['skillLevel'] as number,
          dataVolume: answers['dataVolume'] as number,
          userTasks: answers['userTasks'] as number[],
          budget: answers['budget'] as number,
          teamSize: 0,
          technicalBackground: answers['technicalBackground'] as number,
          integrations: answers['integrations'] as number[]
        }

        const results: RecommendationResult[] = await recommendationService.createSession(payload)

        const botMessages = results.map((r, i) => ({
          id: chat.messages.length + 1 + i,
          author: 'bot' as const,
          text: `${r.resultType === 0 ? 'Основная рекомендация' : 'Альтернатива'}: ${r.toolName}. Описание: ${r.reasoningSummary}`,
          time: new Date().toISOString()
        }))

        updateChat({
          ...chat,
          title: `Сессия ${new Date().toLocaleDateString('ru-RU')}`,
          messages: [...chat.messages, ...botMessages]
        })
      } catch (err) {
        updateChat({
          ...chat,
          messages: [
            ...chat.messages,
            {
              id: chat.messages.length + 1,
              author: 'bot' as const,
              text: 'Произошла ошибка при получении рекомендаций. Проверьте соединение и попробуйте создать новый чат.',
              time: new Date().toISOString()
            }
          ]
        })
      } finally {
        setIsLoading(false)
      }
    }

    fetchRecommendations()
  }, [finished, chat, userId, answers, updateChat])

  if (!chat) {
    return (
      <main className="flex-1 flex items-center justify-center">
        Выберите чат
      </main>
    )
  }

  return (
    <main className="flex-1 flex items-center justify-center">
      <div className="w-full max-w-7xl p-6">
        <div className="bg-[#0b0f1b] rounded-3xl border border-[#2b2f3b] overflow-hidden">
          <ChatHeader title={chat.title} />

          {!finished && <QuestionnaireModule />}

          {finished && <ChatOverview messages={chat.messages} isLoading={isLoading} />}

          <ChatInput
            input={input}
            setInput={setInput}
            sendMessage={sendMessage}
            disabled={!finished || isLoading}
          />
        </div>
      </div>
    </main>
  )
}
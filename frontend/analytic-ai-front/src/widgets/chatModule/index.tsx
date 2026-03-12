import { useState, useEffect, useRef } from 'react'
import { ChatHeader } from '../../features/chatHeader'
import { ChatOverview } from '../../features/chatOverview'
import { ChatInput } from '../../features/chatInput'
import { useChatStore } from '../../store/chat'
import { useQuestionnaireStore } from '../../store/questionnnaire'
import { useAuthStore } from '../../store/auth'
import { QuestionnaireModule } from '../questionnaireModule'

type RecommendationResult = {
  toolName: string
  confidence: number
  reasoningSummary: string
  resultType: number // 0 = Main, 1 = Alternative
}

export const ChatModule = () => {
  const [input, setInput] = useState('')
  const [isLoading, setIsLoading] = useState(false)
  const sentRef = useRef(false)

  const { chats, activeChatId, updateChat } = useChatStore()
  const { finished, answers } = useQuestionnaireStore()
  const { userId } = useAuthStore()

  const chat = chats.find((c) => c.chatId === activeChatId) ?? null

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

  useEffect(() => {
    if (!finished || !chat || !userId || sentRef.current) return

    sentRef.current = true
    setIsLoading(true)

    const payload = {
      userId,
      skillLevel: answers['skillLevel'] as number,
      dataVolume: answers['dataVolume'] as number,
      userTasks: answers['userTasks'] as number[],
      budget: answers['budget'] as number,
      teamSize: 0,
      technicalBackground: answers['technicalBackground'] as number,
      integrations: answers['integrations'] as number[]
    }

    fetch('/api/request/create-new-rec-session', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload)
    })
      .then((res) => {
        if (!res.ok) throw new Error('Ошибка сервера')
        return res.json() as Promise<RecommendationResult[]>
      })
      .then((results) => {
        const resultLabel = (type: number) =>
          type === 0 ? 'Основная рекомендация' : 'Альтернатива'

        const botMessages = results.map((r, i) => ({
          id: chat.messages.length + 1 + i,
          author: 'bot' as const,
          text: `${resultLabel(r.resultType)}: ${r.toolName} (${Math.round(r.confidence * 100)}%)\n${r.reasoningSummary}`,
          time: new Date().toISOString()
        }))

        updateChat({
          ...chat,
          title: `Сессия ${new Date().toLocaleDateString('ru-RU')}`,
          messages: [...chat.messages, ...botMessages]
        })
      })
      .catch(() => {
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
      })
      .finally(() => setIsLoading(false))
  }, [finished])

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

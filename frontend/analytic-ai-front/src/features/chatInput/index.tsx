import { Button } from "../../ui/button"
import { recommendationService } from "../../services/recommendationService"
import type { MessageType } from "../../types/chat"

type Props = {
  messages: MessageType[]
}

export const ChatInput = ({ messages }: Props) => {

  // Находим финальное сообщение с sessionId
  const finalBotMessage = messages.find(m => m.author === 'bot' && m.isHtml && m.sessionId)

  const downloadPdf = async () => {
    if (!finalBotMessage?.sessionId) return

    try {
      const pdfFile = await recommendationService.exportPdf(finalBotMessage.sessionId)
      const url = window.URL.createObjectURL(pdfFile)
      const a = document.createElement("a")
      a.href = url
      a.download = `chat-${finalBotMessage.sessionId}.pdf`
      a.click()
      window.URL.revokeObjectURL(url)
    } catch (err) {
      console.error(err)
      alert("Не удалось скачать PDF. Попробуйте позже.")
    }
  }

  return (
    <div className="flex justify-center p-4 border-t border-[#2b2f3b]">
      <Button
        onClick={downloadPdf}
        disabled={!finalBotMessage}
        className="px-6 py-2"
      >
        Скачать результаты
      </Button>
    </div>
  )
}
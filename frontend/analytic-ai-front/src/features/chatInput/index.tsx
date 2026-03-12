import { Button } from "../../ui/button";

type Props = {
  input: string
  setInput: (value: string) => void
  sendMessage: () => void
  disabled?: boolean
}

export const ChatInput = ({
  input,
  setInput,
  sendMessage,
  disabled = false
}: Props) => {
  return (
    <form
      className="flex gap-3 items-center p-3 border-t border-[#2b2f3b]"
      onSubmit={(e) => {
        e.preventDefault()
        if (!disabled) sendMessage()
      }}
    >
      <input
        disabled={disabled}
        className="flex-1 bg-[#0b0f1b] border border-[#2b2f3b] rounded-2xl p-4 outline-none text-white placeholder:text-[#7b7b8c] disabled:opacity-50 disabled:cursor-not-allowed"
        placeholder={
          disabled
            ? "Сначала ответьте на вопросы выше 👆"
            : "Напишите сообщение..."
        }
        value={input}
        onChange={(e) => setInput(e.target.value)}
      />

      <Button type="submit" disabled={disabled}>
        Отправить
      </Button>
    </form>
  )
}
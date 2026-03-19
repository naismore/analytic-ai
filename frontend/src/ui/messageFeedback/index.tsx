import { useState } from "react"

type Props = {
  messageId: number
}

export const MessageFeedback = ({ messageId: _ }: Props) => {
  const [value, setValue] = useState<"like" | "dislike" | null>(null)

  const sendFeedback = (type: "like" | "dislike") => {
    if (value !== null) return
    setValue(type)
  }

  return (
    <div className="flex gap-2 mt-2 text-sm">
      <button
        onClick={() => sendFeedback("like")}
        disabled={value !== null}
        title={value !== null ? "Оценка уже поставлена" : "Полезно"}
        className={`px-2 py-1 rounded transition ${
          value === "like"
            ? "bg-green-600 text-white"
            : value === null
              ? "bg-[#1a1f2e] hover:bg-[#252b3e]"
              : "bg-[#1a1f2e] opacity-30 cursor-not-allowed"
        }`}
      >
        👍
      </button>

      <button
        onClick={() => sendFeedback("dislike")}
        disabled={value !== null}
        title={value !== null ? "Оценка уже поставлена" : "Не полезно"}
        className={`px-2 py-1 rounded transition ${
          value === "dislike"
            ? "bg-red-600 text-white"
            : value === null
              ? "bg-[#1a1f2e] hover:bg-[#252b3e]"
              : "bg-[#1a1f2e] opacity-30 cursor-not-allowed"
        }`}
      >
        👎
      </button>
    </div>
  )
}

type Props = {
  current: number
  total: number
}

export const QuestionProgress = ({ current, total }: Props) => {
  const percent = Math.min((current / total) * 100, 100)

  return (
    <div className="p-4 border-b border-[#2b2f3b]">
      <div className="flex justify-between text-sm text-[#b7b7c8] mb-2">
        <span>
          Вопрос {Math.min(current + 1, total)}/{total}
        </span>
      </div>

      <div className="w-full h-2 bg-[#1a1f2e] rounded">
        <div
          className="h-2 rounded bg-gradient-to-r from-[#3a3fff] to-[#7c4dff] transition-all"
          style={{ width: `${percent}%` }}
        />
      </div>
    </div>
  )
}
import { useState } from 'react'
import type { QuestionOption } from '../../entities/questions'

type Props = {
  question: string
  options: QuestionOption[]
  multi?: boolean
  onSelect: (value: number | number[]) => void
}

export const QuestionMessage = ({ question, options, multi, onSelect }: Props) => {
  const [selected, setSelected] = useState<number | null>(null)
  const [multiSelected, setMultiSelected] = useState<number[]>([])

  if (multi) {
    const toggle = (value: number) => {
      setMultiSelected((prev) =>
        prev.includes(value) ? prev.filter((v) => v !== value) : [...prev, value]
      )
    }

    return (
      <div className="bg-[#111827] border border-[#2b2f3b] text-white p-4 rounded-2xl max-w-[80%]">
        <p className="mb-3 font-medium">{question}</p>

        <div className="flex flex-col gap-2">
          {options.map((option) => (
            <button
              key={option.value}
              onClick={() => toggle(option.value)}
              className={`px-3 py-2 rounded-xl border text-left transition ${
                multiSelected.includes(option.value)
                  ? 'bg-[#3a3fff] border-[#3a3fff] text-white'
                  : 'bg-[#0b0f1b] border-[#2b2f3b] hover:bg-[#1a1f2e]'
              }`}
            >
              {option.label}
            </button>
          ))}
        </div>

        <button
          onClick={() => {
            if (multiSelected.length > 0) onSelect(multiSelected)
          }}
          disabled={multiSelected.length === 0}
          className="mt-3 px-4 py-2 rounded-xl bg-gradient-to-r from-[#3a3fff] to-[#7c4dff] disabled:opacity-40 disabled:cursor-not-allowed text-sm font-medium transition"
        >
          Продолжить ({multiSelected.length})
        </button>
      </div>
    )
  }

  return (
    <div className="bg-[#111827] border border-[#2b2f3b] text-white p-4 rounded-2xl max-w-[80%]">
      <p className="mb-3 font-medium">{question}</p>

      <div className="flex flex-col gap-2">
        {options.map((option) => (
          <button
            key={option.value}
            disabled={selected !== null}
            onClick={() => {
              setSelected(option.value)
              onSelect(option.value)
            }}
            className={`px-3 py-2 rounded-xl border text-left transition ${
              selected === option.value
                ? 'bg-[#3a3fff] border-[#3a3fff] text-white'
                : selected !== null
                  ? 'bg-[#0b0f1b] border-[#2b2f3b] opacity-40 cursor-not-allowed'
                  : 'bg-[#0b0f1b] border-[#2b2f3b] hover:bg-[#1a1f2e]'
            }`}
          >
            {option.label}
          </button>
        ))}
      </div>
    </div>
  )
}

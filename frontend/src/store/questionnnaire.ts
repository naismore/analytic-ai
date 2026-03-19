import { create } from 'zustand'
import { QUESTIONS } from '../entities/questions'

type QuestionnaireState = {
  currentQuestion: number
  answers: Record<string, number | number[]>
  finished: boolean

  answerQuestion: (field: string, value: number | number[]) => void
  reset: () => void
}

export const useQuestionnaireStore = create<QuestionnaireState>((set) => ({
  currentQuestion: 0,
  answers: {},
  finished: false,

  answerQuestion: (field, value) =>
    set((state) => {
      const next = state.currentQuestion + 1
      return {
        answers: { ...state.answers, [field]: value },
        currentQuestion: next,
        finished: next >= QUESTIONS.length
      }
    }),

  reset: () =>
    set({
      currentQuestion: 0,
      answers: {},
      finished: false
    })
}))

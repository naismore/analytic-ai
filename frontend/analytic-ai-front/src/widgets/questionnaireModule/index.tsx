import { QUESTIONS } from "../../entities/questions"
import { useQuestionnaireStore } from "../../store/questionnnaire"
import { QuestionMessage } from "../../ui/questionMessage"
import { QuestionProgress } from "../../ui/questionProgress"

export const QuestionnaireModule = () => {
  const { currentQuestion, answerQuestion, finished } =
    useQuestionnaireStore()

  if (finished) return null

  const question = QUESTIONS[currentQuestion]

  if (!question) return null

  return (
    <div>

      <QuestionProgress
        current={currentQuestion}
        total={QUESTIONS.length}
      />

      <div className="p-5 h-[571px]">
        <QuestionMessage
          question={question.question}
          options={question.options}
          multi={question.multi}
          onSelect={(value) => answerQuestion(question.field, value)}
        />
      </div>

    </div>
  )
}
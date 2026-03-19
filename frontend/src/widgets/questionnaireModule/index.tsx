import { QUESTIONS } from "../../entities/questions"
import { useChatStore } from "../../store/chat"
import { QuestionMessage } from "../../ui/questionMessage"
import { QuestionProgress } from "../../ui/questionProgress"

export const QuestionnaireModule = () => {
  const { chats, activeChatId, answerQuestion } = useChatStore()
  const activeChat = chats.find(c => c.chatId === activeChatId)
  const questionnaire = activeChat?.questionnaire ?? { currentQuestion: 0, answers: {}, finished: false }
  const { currentQuestion, finished } = questionnaire

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
          key={currentQuestion}
          question={question.question}
          options={question.options}
          multi={question.multi}
          onSelect={(value) => answerQuestion(question.field, value)}
        />
      </div>

    </div>
  )
}
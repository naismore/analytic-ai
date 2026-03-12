import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import { useAuthStore } from '../../store/auth'

const LoginPage = () => {

  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [mode, setMode] = useState<'login' | 'register'>('login')
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)

  const { login, register, accessToken } = useAuthStore()

  const navigate = useNavigate()

  // если пользователь уже авторизован → редирект
  useEffect(() => {
    console.log(accessToken)
    if (accessToken) {
      navigate('/chat')
    }
  }, [accessToken, navigate])

  const handle = async () => {

    setError('')
    setLoading(true)

    try {

      if (mode === 'register') {
        await register(username, password)
      }

      await login(username, password)

      navigate('/chat')

    } catch (e: unknown) {

      setError(e instanceof Error ? e.message : 'Произошла ошибка')

    } finally {

      setLoading(false)

    }
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-b from-[#0b0f1b] to-[#0a0c14] text-white">
      <div className="bg-[#111827] border border-[#2b2f3b] rounded-3xl p-8 w-full max-w-sm">

        <h2 className="text-2xl font-bold mb-6 text-center">
          {mode === 'login' ? 'Вход' : 'Регистрация'}
        </h2>

        <div className="flex flex-col gap-4">

          <input
            className="bg-[#0b0f1b] border border-[#2b2f3b] rounded-xl p-3 outline-none text-white placeholder:text-[#7b7b8c]"
            placeholder="Имя пользователя"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />

          <input
            type="password"
            className="bg-[#0b0f1b] border border-[#2b2f3b] rounded-xl p-3 outline-none text-white placeholder:text-[#7b7b8c]"
            placeholder="Пароль"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            onKeyDown={(e) => e.key === 'Enter' && handle()}
          />

          {error && (
            <p className="text-red-400 text-sm text-center">
              {error}
            </p>
          )}

          <button
            onClick={handle}
            disabled={loading || !username || !password}
            className="bg-gradient-to-r from-[#3a3fff] to-[#7c4dff] rounded-xl p-3 font-semibold disabled:opacity-50 disabled:cursor-not-allowed transition"
          >
            {loading
              ? 'Загрузка...'
              : mode === 'login'
                ? 'Войти'
                : 'Зарегистрироваться'}
          </button>

          <button
            onClick={() => {
              setMode(mode === 'login' ? 'register' : 'login')
              setError('')
            }}
            className="text-[#7b7b8c] text-sm hover:text-white transition"
          >
            {mode === 'login'
              ? 'Нет аккаунта? Зарегистрироваться'
              : 'Уже есть аккаунт? Войти'}
          </button>

        </div>
      </div>
    </div>
  )
}

export default LoginPage
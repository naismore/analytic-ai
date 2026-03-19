import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import { useAuthStore } from '../../store/auth'

const IDENTITY_TRANSLATIONS: [RegExp, string][] = [
  [/Passwords must have at least one digit/i, 'Пароль должен содержать хотя бы одну цифру (0–9)'],
  [/Passwords must have at least one uppercase/i, 'Пароль должен содержать хотя бы одну заглавную букву (A–Z)'],
  [/Passwords must have at least one lowercase/i, 'Пароль должен содержать хотя бы одну строчную букву (a–z)'],
  [/Passwords must have at least one non alphanumeric/i, 'Пароль должен содержать хотя бы один специальный символ'],
  [/Passwords must be at least (\d+) characters/i, 'Пароль должен быть не короче $1 символов'],
  [/Passwords must be/i, 'Пароль не соответствует требованиям'],
  [/Username .+ is already taken/i, 'Это имя пользователя уже занято'],
  [/Email .+ is already taken/i, 'Этот email уже используется'],
  [/Invalid username or password/i, 'Неверное имя пользователя или пароль'],
  [/User not found/i, 'Пользователь не найден'],
]

const translateIdentityErrors = (text: string): string => {
  let result = text
  for (const [pattern, translation] of IDENTITY_TRANSLATIONS) {
    result = result.replace(pattern, translation)
  }
  return result
}

const parseError = (raw: string): string => {
  // Пробуем распарсить JSON-ошибку
  try {
    const json = JSON.parse(raw)
    const message = json?.errors?.[0] ?? json?.message ?? json?.title ?? null
    if (message) return translateIdentityErrors(String(message))
  } catch {}

  // Вытаскиваем текст после "Exception: " и до первого " at "
  const match = raw.match(/Exception:\s*(.+?)(?:\s+at\s|\s*$)/s)
  const message = match ? match[1].trim() : raw.length < 300 ? raw : null
  if (!message) return 'Произошла ошибка. Попробуйте ещё раз.'
  return translateIdentityErrors(message)
}

const LoginPage = () => {

  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [mode, setMode] = useState<'login' | 'register'>('login')
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)

  const { login, register, accessToken } = useAuthStore()

  const navigate = useNavigate()

  useEffect(() => {
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

      setError(e instanceof Error ? parseError(e.message) : 'Произошла ошибка')

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
            <div className="flex items-start gap-2 bg-red-500/10 border border-red-500/30 rounded-xl px-3 py-2.5 text-sm text-red-400">
              <svg className="shrink-0 mt-0.5" xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                <circle cx="12" cy="12" r="10" />
                <line x1="12" y1="8" x2="12" y2="12" />
                <line x1="12" y1="16" x2="12.01" y2="16" />
              </svg>
              <span>{error}</span>
            </div>
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
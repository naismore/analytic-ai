import { Routes, Route, Navigate } from 'react-router-dom'
import MainPage from './pages/mainPage'
import ChatPage from './pages/chatPage'
import LoginPage from './pages/loginPage'
import { useAuthStore } from './store/auth'
import { useEffect } from 'react'

export default function App() {
  const { accessToken, initialized } = useAuthStore()
  const refresh = useAuthStore(s => s.refresh)

  // вызываем refresh один раз при монтировании App
  useEffect(() => {
    refresh()
  }, [])

  console.log(initialized)

  // пока не проверили refresh → loader
  if (!initialized) {
    return (
      <div className="h-screen flex items-center justify-center text-white">
        Loading...
      </div>
    )
  }

  return (
    <Routes>
      {/* если пользователь уже залогинен → редирект с login */}
      <Route path="/login" element={accessToken ? <Navigate to="/" replace /> : <LoginPage />} />
      
      {/* приватные страницы */}
      <Route path="/" element={accessToken ? <MainPage /> : <Navigate to="/login" replace />} />
      <Route path="/chat" element={accessToken ? <ChatPage /> : <Navigate to="/login" replace />} />
    </Routes>
  )
}
import { useAuthStore } from '../store/auth'
import { authService } from './authService'

const BASE_URL = '/api'

type RequestOptions = RequestInit & {
  auth?: boolean
  retry?: boolean
  responseType?: 'json' | 'blob' // <-- добавили поддержку blob
}

export async function apiClient<T>(
  endpoint: string,
  options: RequestOptions = {}
): Promise<T> {
  const { auth = true, retry = true, headers, responseType = 'json', ...rest } = options

  const token = useAuthStore.getState().accessToken

  const requestHeaders: Record<string, string> = {
    'Content-Type': 'application/json',
    ...(headers as Record<string, string> || {})
  }

  if (auth && token) {
    requestHeaders['Authorization'] = `Bearer ${token}`
  }

  const res = await fetch(`${BASE_URL}${endpoint}`, {
    ...rest,
    headers: requestHeaders,
    credentials: 'include'
  })

  // --- обновляем токен если 401 ---
  if (res.status === 401 && retry) {
    try {
      const data = await authService.refresh() // auth: false, retry: false

      useAuthStore.setState({
        userId: data.userId,
        userName: data.userName,
        accessToken: data.accessToken
      })

      return apiClient<T>(endpoint, { ...options, retry: false })
    } catch {
      useAuthStore.getState().logout()
      throw new Error('Session expired')
    }
  }

  if (!res.ok) {
    const text = await res.text()
    throw new Error(text || 'API Error')
  }

  // --- если нужно получить blob (для PDF) ---
  if (responseType === 'blob') {
    const blob = await res.blob()
    return blob as unknown as T
  }

  // --- обычный JSON ---
  const text = await res.text()
  return text ? JSON.parse(text) : undefined as unknown as T
}
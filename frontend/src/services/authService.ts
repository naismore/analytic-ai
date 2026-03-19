import type { LoginResponse } from '../entities/loginResponse'
import { apiClient } from './apiClient'

export const authService = {

  login(username: string, password: string) {
    return apiClient<LoginResponse>('/auth/login', {
      method: 'POST',
      auth: false,
      body: JSON.stringify({ username, password })
    })
  },

  register(username: string, password: string) {
    return apiClient<void>('/auth/register', {
      method: 'POST',
      auth: false,
      body: JSON.stringify({ username, password })
    })
  },

  refresh() {
    return apiClient<LoginResponse>('/auth/refresh-token', {
      method: 'POST',
      auth: false,
      retry: false,
    })
  }

}
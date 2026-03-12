import { create } from 'zustand'
import { authService } from '../services/authService'
import { useChatStore } from './chat'

type AuthState = {
  userId: number | null
  userName: string | null
  accessToken: string | null
  initialized: boolean

  login: (username: string, password: string) => Promise<void>
  register: (username: string, password: string) => Promise<void>
  refresh: () => Promise<void>
  logout: () => void
}

export const useAuthStore = create<AuthState>((set) => ({
  userId: null,
  userName: null,
  accessToken: null,
  initialized: false,

  login: async (username, password) => {
    const data = await authService.login(username, password)
    set({
      userId: data.userId,
      userName: data.userName,
      accessToken: data.accessToken
    });

    // загружаем историю чатов после успешного входа
    useChatStore.getState().loadChatsFromServer(data.userId)
  },

  register: async (username, password) => {
    await authService.register(username, password)
  },

  refresh: async () => {
    try {
      const data = await authService.refresh()
      set({
        userId: data.userId,
        userName: data.userName,
        accessToken: data.accessToken,
        initialized: true
      });

      // загружаем историю чатов после refresh
      useChatStore.getState().loadChatsFromServer(data.userId)
    } catch {
      set({
        userId: null,
        userName: null,
        accessToken: null,
        initialized: true
      });

      // очистка чатов при неудачном refresh
      useChatStore.getState().clearChats()
    }
  },

  logout: () => {
    set({
      userId: null,
      userName: null,
      accessToken: null
    })

    // очищаем историю чатов
    useChatStore.getState().clearChats()
  }
}))
import { apiClient } from './apiClient'

export type CreateRecSessionRequest = {
  userId: number
  skillLevel: number
  dataVolume: number
  userTasks: number[]
  budget: number
  teamSize: number
  technicalBackground: number
  integrations: number[]
}

export type RecommendationSessionResponse = {
  sessionId: string
  results: RecommendationResult[]
}

export type RecommendationResult = {
  toolName: string
  confidence: number
  reasoningSummary: string
  resultType: number
}

export type RecommendationSession = {
  sessionId: string
  skillLevel: number
  dataVolume: number
  userTasks: number[]
  budget: number
  teamSize: number
  technicalBackground: number
  integrations: number[],
  results: RecommendationResult[],
}

export type RecommendationSessionsList = {
  sessionId: string
  name: string
  createdAt: string
}

export const recommendationService = {

  createSession(data: CreateRecSessionRequest) {
    return apiClient<RecommendationSessionResponse>(
      '/request/create-new-rec-session',
      {
        method: 'POST',
        auth: true,
        body: JSON.stringify(data)
      }
    )
  },

  getSessionsList(userId: number) {
    return apiClient<RecommendationSessionsList[]>(
      `/request/get-recommendation-sessions/${userId}`,
      {
        method: 'GET',
        auth: true
      }
    )
  },

  getSession(id: string) {
    return apiClient<RecommendationSession>(
      `/request/get-recommendation-session/${id}`,
      {
        method: 'GET',
        auth: true
      }
    )
  },

  exportPdf(id: string) {
    return apiClient<File>(`/request/export-pdf/${id}`, {
      method: 'GET',
      auth: true,
      responseType: 'blob' // <-- важно!
    })
}

}
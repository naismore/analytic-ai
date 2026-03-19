export type LoginResponse = {
  userId: number
  userName: string
  accessToken: string
  accessTokenExpiresAt: string
  refreshTokenExpiresAt: string
}
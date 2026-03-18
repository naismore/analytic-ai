@startuml
!theme plain
left to right direction

skinparam componentStyle uml2
skinparam linetype ortho

' Границы слоев
rectangle Presentation {
  [AuthController] as AuthController
  [RecommendationSessionController] as RecSessionController
}

rectangle Application {
  rectangle "CQRS Handlers" {
    [RegisterUserCommandHandler]
    [LoginUserCommandHandler]
    [RefreshTokenCommandHandler]
    [CreateNewRecSessionCommandHandler]
    [GetRecSessionByIdQueryHandler]
    [GetRecSessionsByUserIdQueryHandler]
  }
  
  rectangle "Abstract Interfaces" {
    interface IAuthService
    interface ITokenService
    interface IUserIdentityService
    interface ILLMService
    interface IEnumResolver
    interface IRecommendationParser
    
    interface IUserRepository
    interface IRecommendationSessionRepository
    interface IRecommendationResultRepository
    interface IRecommendationAttributesRepository
    interface IRefreshTokenRepository
  }
}

rectangle Domain {
  [User]
  [RecommendationSession]
  [RecommendationResult]
  [RecommendationAttributes]
  
  interface "IRepository<T>" as IRepository
}

rectangle Infrastructure {
  rectangle "Identity Services" {
    [AuthService]
    [UserIdentityService]
  }
  
  rectangle "Token Management" {
    [TokenService] as TokenService
  }
  
  rectangle "External Services" {
    [LLMService]
  }
  
  rectangle "Repositories" {
    [UserRepository]
    [RecommendationSessionRepository]
    [RecommendationResultRepository]
    [RecommendationAttributesRepository]
    [RefreshTokenRepository]
  }
  
  rectangle "Utilities" {
    [EnumResolver]
    [RecommendationParser]
  }
  
  database "Database" as DB
}

' Презентационный слой -> Application
AuthController --> RegisterUserCommandHandler : sends
AuthController --> LoginUserCommandHandler : sends
AuthController --> RefreshTokenCommandHandler : sends

RecSessionController --> CreateNewRecSessionCommandHandler : sends
RecSessionController --> GetRecSessionByIdQueryHandler : sends
RecSessionController --> GetRecSessionsByUserIdQueryHandler : sends

' Application Handlers -> Abstract Interfaces
RegisterUserCommandHandler --> IUserRepository
RegisterUserCommandHandler --> IAuthService

LoginUserCommandHandler --> IAuthService

RefreshTokenCommandHandler --> ITokenService
RefreshTokenCommandHandler --> IRefreshTokenRepository
RefreshTokenCommandHandler --> IUserIdentityService

CreateNewRecSessionCommandHandler --> IRecommendationSessionRepository
CreateNewRecSessionCommandHandler --> IRecommendationResultRepository
CreateNewRecSessionCommandHandler --> IRecommendationAttributesRepository
CreateNewRecSessionCommandHandler --> ILLMService
CreateNewRecSessionCommandHandler --> IEnumResolver
CreateNewRecSessionCommandHandler --> IRecommendationParser

GetRecSessionByIdQueryHandler --> IRecommendationAttributesRepository
GetRecSessionsByUserIdQueryHandler --> IRecommendationSessionRepository
GetRecSessionsByUserIdQueryHandler --> IRecommendationAttributesRepository

' Infrastructure implements Application interfaces
AuthService -up-|> IAuthService
UserIdentityService -up-|> IUserIdentityService
TokenService -up-|> ITokenService
LLMService -up-|> ILLMService
EnumResolver -up-|> IEnumResolver
RecommendationParser -up-|> IRecommendationParser

UserRepository -up-|> IUserRepository
UserRepository -up-|> IRepository

RecommendationSessionRepository -up-|> IRecommendationSessionRepository
RecommendationSessionRepository -up-|> IRepository

RecommendationResultRepository -up-|> IRecommendationResultRepository
RecommendationResultRepository -up-|> IRepository

RecommendationAttributesRepository -up-|> IRecommendationAttributesRepository
RecommendationAttributesRepository -up-|> IRepository

RefreshTokenRepository -up-|> IRefreshTokenRepository
RefreshTokenRepository -up-|> IRepository

' Domain Entities used by Repositories
UserRepository --> User
RecommendationSessionRepository --> RecommendationSession
RecommendationResultRepository --> RecommendationResult
RecommendationAttributesRepository --> RecommendationAttributes

' Infrastructure dependencies
AuthService --> UserIdentityService : uses
AuthService --> TokenService : uses
TokenService --> RefreshTokenRepository : uses

UserRepository --> DB : persists
RecommendationSessionRepository --> DB : persists
RecommendationResultRepository --> DB : persists
RecommendationAttributesRepository --> DB : persists
RefreshTokenRepository --> DB : persists

LLMService --> "External LLM API" : HTTP calls

note top of AuthController : REST Endpoints:\nPOST /register\nPOST /login\nPOST /refresh-token
note top of RecSessionController : REST Endpoints:\nPOST /create-new-rec-session\nGET /get-recommendation-sessions/{userId}\nGET /get-recommendation-session/{sessionId}
note bottom of CreateNewRecSessionCommandHandler : Orchestrates:\n- Calls LLM\n- Parses response\n- Saves session & results
note bottom of Domain : Core business entities\nand repository interfaces

@enduml

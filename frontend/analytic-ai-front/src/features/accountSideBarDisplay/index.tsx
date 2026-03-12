import { useAuthStore } from '../../store/auth'

export const AccountSideBarDisplay = () => {
  const { userName, logout } = useAuthStore()

  return (
    <div className="flex items-center gap-3">
      <div className="w-12 h-12 rounded-full bg-gradient-to-br from-[#36c0ff] to-[#7c4dff] shrink-0" />
      <div className="flex-1 min-w-0">
        <div className="font-bold truncate">{userName ?? 'Пользователь'}</div>
      </div>
      <button
        onClick={logout}
        title="Выйти"
        className="text-[#7b7b8c] hover:text-white transition shrink-0"
      >
        <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
          <path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4" />
          <polyline points="16 17 21 12 16 7" />
          <line x1="21" y1="12" x2="9" y2="12" />
        </svg>
      </button>
    </div>
  )
}
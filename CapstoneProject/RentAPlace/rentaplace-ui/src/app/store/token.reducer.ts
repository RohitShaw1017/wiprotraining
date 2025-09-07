import { createReducer, on, Action } from '@ngrx/store';
import { setToken, clearToken } from './token.actions';

// Our token can be either a string (JWT) or null
export type TokenState = string | null;

export const initialState: TokenState = null;

// reducer function
const _tokenReducer = createReducer<TokenState>(
  initialState,
  on(setToken, (_state, { token }) => token),   // token is a string ✅
  on(clearToken, (_state) => null)              // null is valid ✅
);

export function tokenReducer(state: TokenState | undefined, action: Action): TokenState {
  return _tokenReducer(state, action);
}

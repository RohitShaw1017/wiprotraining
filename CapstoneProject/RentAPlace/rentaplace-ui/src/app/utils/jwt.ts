export function parseJwt(token: string): any {
  try {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );
    return JSON.parse(jsonPayload);
  } catch {
    return null;
  }
}

export function extractRoleFromJwt(token: string): string | null {
  const payload = parseJwt(token);
  if (!payload) return null;

  // role may be under 'role' or in the standard claim URI (from your backend)
  const uriRole = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
  return payload.role ?? payload[uriRole] ?? null;
}

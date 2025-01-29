export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  username: string;
  role: string;
  token: string;
}

export interface RegisterRequest {
  username: string;
  password: string;
  role: string;
}

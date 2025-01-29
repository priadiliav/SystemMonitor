import { Injectable } from '@angular/core';
import { LoginRequest, LoginResponse, RegisterRequest } from '../models/user';
import { LocalStorageService } from './local-storage.service';
import { HttpClient } from '@microsoft/signalr';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(
    private httpClient: HttpClient,
    private localStorageService: LocalStorageService
  ) {}
  public login(loginRequest: LoginRequest): void {}
  public register(registerRequest: RegisterRequest): void {}
}

import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
class LocalStorageService {
  constructor() {}

  public setItem(key: string, value: any): void {
    localStorage.setItem(key, JSON.stringify(value));
  }

  public getItem(key: string): any {
    const item = localStorage.getItem(key);
    if (item) {
      return JSON.parse(item);
    }
    return null;
  }
}

import React, { createContext, useCallback, useState, useContext } from 'react';

import api from '../services/api';

interface User {
  id: string;
  userName: string;
}

interface AuthState {
  token: string;
  user: User;
}

interface SignInCredentials {
  userName: string;
  password: string;
}

interface AuthContextData {
  user: User;
  signIn(credentials: SignInCredentials): Promise<void>;
  signOut(): void;
  updateUser(user: User): void;
}

export const AuthContext = createContext<AuthContextData>(
  {} as AuthContextData,
);

export const AuthProvider: React.FC = ({ children }) => {
  const [data, setData] = useState<AuthState>(() => {
    const token = localStorage.getItem('@SolarCalculator:token');
    const user = localStorage.getItem('@SolarCalculator:user');

    if (token && user) {
      api.defaults.headers.authorization = `Bearer ${token}`;

      return { token, user: JSON.parse(user) };
    }

    return {} as AuthState;
  });

  const signIn = useCallback(async ({ userName, password }) => {
    const response = await api.post('v1/login', {
      userName,
      password,
    });

    const { token, user } = response.data;
    localStorage.setItem('@SolarCalculator:token', token);
    localStorage.setItem('@SolarCalculator:user', JSON.stringify(user));

    api.defaults.headers.authorization = `Bearer ${token}`;

    setData(response.data);
  }, []);

  const signOut = useCallback(() => {
    localStorage.removeItem('@SolarCalculator:token');
    localStorage.removeItem('@SolarCalculator:user');

    setData({} as AuthState);
  }, [setData]);

  const updateUser = useCallback(
    (user: User) => {
      localStorage.setItem('@SolarCalculator:user', JSON.stringify(user));

      setData({
        token: data.token,
        user,
      });
    },
    [setData, data.token],
  );

  return (
    <AuthContext.Provider
      value={{ user: data.user, signIn, signOut, updateUser }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export function useAuth(): AuthContextData {
  const context = useContext(AuthContext);

  return context;
}

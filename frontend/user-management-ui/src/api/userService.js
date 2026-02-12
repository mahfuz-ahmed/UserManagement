import { apiClient } from './apiClient';

export const userService = {
    fetchUsers: async () => {
        return await apiClient('/users/fetch-users');
    },

    createUser: async (userData) => {
        return await apiClient('/users/create-users', {
            method: 'POST',
            body: userData
        });
    },

    createBulkUsers: async () => {
        return await apiClient('/users/create-bulk-users', {
            method: 'POST'
        });
    }
};

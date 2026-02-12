const API_BASE_URL = 'http://localhost:5168/api';

export const apiClient = async (endpoint, options = {}) => {
    const { method = 'GET', body, headers = {}, ...customConfig } = options;

    const config = {
        method,
        ...customConfig,
        headers: {
            ...headers,
        },
    };

    if (body) {
        config.body = JSON.stringify(body);
        config.headers['Content-Type'] = 'application/json';
    }

    const response = await fetch(`${API_BASE_URL}${endpoint}`, config);

    if (response.ok) {
        // Only try to parse as JSON if there's content
        const contentType = response.headers.get('content-type');
        if (contentType && contentType.includes('application/json')) {
            return await response.json();
        }
        return response; // Return response for 200 OK with no body
    } else {
        const errorData = await response.json().catch(() => ({}));
        const error = new Error(errorData.message || 'API request failed');
        error.status = response.status;
        error.data = errorData;
        throw error;
    }
};

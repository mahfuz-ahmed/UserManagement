import React, { useState } from 'react';

const UserForm = ({ onUserCreated }) => {
    const [formData, setFormData] = useState({
        name: '',
        age: '',
        email: ''
    });
    const [errorMessage, setErrorMessage] = useState('');
    const [successMessage, setSuccessMessage] = useState('');
    const [submitting, setSubmitting] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setSubmitting(true);
        setErrorMessage('');
        setSuccessMessage('');
        try {
            const response = await fetch('http://localhost:5168/api/users/create-users', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    ...formData,
                    age: parseInt(formData.age)
                })
            });

            if (response.ok) {
                setFormData({ name: '', age: '', email: '' });
                setSuccessMessage('User registered successfully!');
                onUserCreated();
            } else {
                const errorData = await response.json();
                setErrorMessage(errorData.message || 'Failed to create user.');
            }
        } catch (error) {
            console.error('Error creating user:', error);
            setErrorMessage('Network error. Please ensure the backend is running.');
        } finally {
            setSubmitting(false);
        }
    };

    return (
        <div className="glass-card">
            <h2>Add New User</h2>

            {successMessage && <div style={{ color: '#22c55e', marginBottom: '1rem', padding: '0.5rem', borderRadius: '4px', background: 'rgba(34, 197, 94, 0.1)' }}>{successMessage}</div>}
            {errorMessage && <div style={{ color: '#ef4444', marginBottom: '1rem', padding: '0.5rem', borderRadius: '4px', background: 'rgba(239, 68, 68, 0.1)' }}>{errorMessage}</div>}

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Full Name</label>
                    <input
                        type="text"
                        value={formData.name}
                        onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                        required
                        placeholder="John Doe"
                    />
                </div>
                <div className="form-group">
                    <label>Email Address</label>
                    <input
                        type="email"
                        value={formData.email}
                        onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                        required
                        placeholder="john@example.com"
                    />
                </div>
                <div className="form-group">
                    <label>Age</label>
                    <input
                        type="number"
                        value={formData.age}
                        onChange={(e) => setFormData({ ...formData, age: e.target.value })}
                        required
                        min="1"
                        placeholder="25"
                    />
                </div>
                <button type="submit" className="btn" disabled={submitting}>
                    {submitting ? 'Creating...' : 'Register User'}
                </button>
            </form>
        </div>
    );
};

export default UserForm;

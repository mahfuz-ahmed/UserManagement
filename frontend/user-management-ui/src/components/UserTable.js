import React, { useEffect, useState } from 'react';

const UserTable = ({ refreshTrigger }) => {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [timeTaken, setTimeTaken] = useState(0);

    const fetchUsers = async () => {
        setLoading(true);
        const start = performance.now();
        try {
            const response = await fetch('http://localhost:5168/api/users/fetch-users');
            const data = await response.json();
            setUsers(data);
        } catch (error) {
            console.error('Error fetching users:', error);
        } finally {
            const end = performance.now();
            setTimeTaken(end - start);
            setLoading(false);
        }
    };

    const handleBulkCreate = async () => {
        setLoading(true);
        try {
            await fetch('http://localhost:5168/api/users/create-bulk-users', { method: 'POST' });
            await fetchUsers();
        } catch (error) {
            console.error('Error creating bulk users:', error);
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchUsers();
    }, [refreshTrigger]);

    return (
        <div className="glass-card">
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <h2>User Directory</h2>
                <div>
                    <span style={{ marginRight: '1rem', color: 'var(--text-muted)' }}>
                        Last fetch: {timeTaken.toFixed(2)}ms
                    </span>
                    <button className="btn btn-outline" onClick={handleBulkCreate} disabled={loading}>
                        {loading ? 'Processing...' : 'Create 10,000 Users'}
                    </button>
                </div>
            </div>

            {loading && users.length === 0 ? (
                <div style={{ display: 'flex', justifyContent: 'center', padding: '2rem' }}>
                    <div className="loader"></div>
                </div>
            ) : (
                <div style={{ maxHeight: '600px', overflowY: 'auto' }}>
                    <table>
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Email</th>
                                <th>Age</th>
                                <th>Created</th>
                            </tr>
                        </thead>
                        <tbody>
                            {users.slice(0, 100).map((user) => (
                                <tr key={user.id}>
                                    <td>{user.name}</td>
                                    <td>{user.email}</td>
                                    <td>{user.age}</td>
                                    <td>{new Date(user.timeStamp).toLocaleString()}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                    {users.length > 100 && (
                        <p style={{ textAlign: 'center', color: 'var(--text-muted)', marginTop: '1rem' }}>
                            Showing first 100 of {users.length} users
                        </p>
                    )}
                </div>
            )}
        </div>
    );
};

export default UserTable;

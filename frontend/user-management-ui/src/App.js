import React, { useState } from 'react';
import UserTable from './components/UserTable';
import UserForm from './components/UserForm';

function App() {
  const [refreshTrigger, setRefreshTrigger] = useState(0);

  const handleUserCreated = () => {
    setRefreshTrigger(prev => prev + 1);
  };

  return (
    <div className="container">
      <header style={{ marginBottom: '3rem', textAlign: 'center' }}>
        <h1>User Management System</h1>
        <p style={{ color: 'var(--text-muted)' }}>High-performance user database with SQLite and React</p>
      </header>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 2fr', gap: '2rem' }}>
        <div>
          <UserForm onUserCreated={handleUserCreated} />
        </div>
        <div>
          <UserTable refreshTrigger={refreshTrigger} />
        </div>
      </div>
    </div>
  );
}

export default App;

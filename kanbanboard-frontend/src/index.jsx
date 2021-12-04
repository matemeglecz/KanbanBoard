import React from 'react';
import ReactDOM from 'react-dom';
import './static/index.css';
import LocalizationProvider from '@mui/lab/LocalizationProvider';
import dateAdapter from '@mui/lab/AdapterDateFns';
import App from './Components/App';

ReactDOM.render(
    <React.StrictMode>
        <LocalizationProvider dateAdapter={dateAdapter}>
            <App />
        </LocalizationProvider>
    </React.StrictMode>,
    document.getElementById('root'),
);

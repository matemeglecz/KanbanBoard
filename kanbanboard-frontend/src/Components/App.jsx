import React from 'react';
import '../static/App.css';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import KanbanBoard from './KanbanBoard';

const themeCustom = createTheme({
    palette: {
        background: {
            default: '#e0e0e0',
        },
        primary: {
            main: '#424242',
        },
        secondary: {
            main: '#616161',
        },
    },
});

function App() {
    return (
        <ThemeProvider theme={themeCustom}>
            <CssBaseline />
            <Box sx={{ flexGrow: 1 }}>
                <AppBar position="static">
                    <Toolbar variant="dense">
                        <Typography
                            variant="h6"
                            color="inherit"
                            component="div"
                        >
                            Tasks
                        </Typography>
                    </Toolbar>
                </AppBar>
            </Box>
            <KanbanBoard />
        </ThemeProvider>
    );
}

export default App;

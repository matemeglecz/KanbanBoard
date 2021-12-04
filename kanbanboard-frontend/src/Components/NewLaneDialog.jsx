import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';
import AddIcon from '@mui/icons-material/Add';
import Grid from '@mui/material/Grid';
import PropTypes from 'prop-types';

export default function NewLaneDialog(props) {
    const { onNewLane } = props;

    const [title, setTitle] = React.useState('');

    const [order, setOrder] = React.useState(1);

    const [open, setOpen] = React.useState(false);

    const [openSnackBar, setOpenSnackBar] = React.useState(false);

    const handleChangeTitle = (event) => {
        setTitle(event.target.value);
    };

    const handleChangeOrder = (event) => {
        setOrder(event.target.value);
    };

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleSave = () => {
        if (title && order) {
            onNewLane({
                title,
                order: order - 1,
            });
            setOpen(false);
        } else {
            setOpenSnackBar(true);
        }
    };

    const handleCloseSnackBar = () => {
        setOpenSnackBar(false);
    };

    return (
        <Grid
            container
            spacing={0}
            item
            alignItems="stretch"
            direction="row"
            xs={12}
            sm={6}
            md={4}
            lg={2}
            sx={{ maxWidth: 1, marginTop: 1 }}
        >
            <Button
                variant="outlined"
                sx={{
                    height: 1, width: 1, minHeight: '50px', maxWidth: 1,
                }}
                onClick={handleClickOpen}
            >
                <AddIcon fontSize="large" />
            </Button>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>Add new lane</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Fill out the fields and add to the board.
                    </DialogContentText>
                    <TextField
                        autoFocus
                        required
                        margin="dense"
                        id="name"
                        label="Title"
                        type="title"
                        fullWidth
                        variant="outlined"
                        onChange={handleChangeTitle}
                        defaultValue={title}
                    />
                    <TextField
                        margin="dense"
                        id="order"
                        label="Order"
                        type="number"
                        fullWidth
                        variant="outlined"
                        onChange={handleChangeOrder}
                        defaultValue={order}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} variant="outlined">
                        Cancel
                    </Button>
                    <Button onClick={handleSave} variant="outlined">
                        Add
                    </Button>
                </DialogActions>
                <Snackbar
                    open={openSnackBar}
                    autoHideDuration={6000}
                    onClose={handleCloseSnackBar}
                    anchorOrigin={{ horizontal: 'center', vertical: 'top' }}
                >
                    <Alert
                        onClose={handleCloseSnackBar}
                        severity="error"
                        sx={{ width: '100%' }}
                    >
                        Title and order are required fields.
                    </Alert>
                </Snackbar>
            </Dialog>
        </Grid>
    );
}

NewLaneDialog.propTypes = {
    onNewLane: PropTypes.func.isRequired,
};

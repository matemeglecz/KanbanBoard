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
import EditIcon from '@mui/icons-material/Edit';
import IconButton from '@mui/material/IconButton';
import PropTypes from 'prop-types';

export default function EditNewCardDialog(props) {
    const {
        title: titleProp,
        mode,
        description: descriptionProp,
        deadline: deadlineProp,
        onSaveCard,
    } = props;

    const editMode = (mode === 'edit');
    const [title, setTitle] = React.useState(
        titleProp,
    );
    const [description, setDescription] = React.useState(
        descriptionProp,
    );
    const [date, setDate] = React.useState(
        new Date(deadlineProp).toLocaleDateString('en-CA'),
    );

    const [open, setOpen] = React.useState(false);

    const [openSnackBar, setOpenSnackBar] = React.useState(false);

    const handleChangeTitle = (event) => {
        setTitle(event.target.value);
    };

    const handleChangeDescription = (event) => {
        setDescription(event.target.value);
    };

    const handleChangeDate = (event) => {
        setDate(event.target.value);
    };

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleSave = () => {
        if (title && date) {
            onSaveCard({
                title,
                description: description || '',
                deadline: date,
            });
            setOpen(false);
        } else {
            setOpenSnackBar(true);
        }
    };

    const handleCloseSnackBar = () => {
        setOpenSnackBar(false);
    };

    const renderButton = () => {
        if (editMode) {
            return (
                <IconButton
                    aria-label="edit"
                    color="primary"
                    size="small"
                    onClick={handleClickOpen}
                >
                    <EditIcon fontSize="small" />
                </IconButton>
            );
        }

        return (
            <Button
                variant="contained"
                onClick={handleClickOpen}
                sx={{ width: 1 }}
                color="primary"
            >
                New Card
            </Button>
        );
    };

    return (
        <div>
            {renderButton()}
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>
                    {editMode ? 'Edit card' : 'Add new card'}
                </DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        {editMode
                            ? 'Edit the card.'
                            : 'Fill out the fields and add to current lane.'}
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
                        id="description"
                        label="Description"
                        type="text"
                        fullWidth
                        multiline
                        rows={3}
                        variant="outlined"
                        onChange={handleChangeDescription}
                        defaultValue={description}
                    />
                    <TextField
                        id="date"
                        label=""
                        type="date"
                        onChange={handleChangeDate}
                        defaultValue={date}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} variant="outlined">
                        Cancel
                    </Button>
                    <Button onClick={handleSave} variant="outlined">
                        {editMode ? 'Edit' : 'Add'}
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
                        Title and deadline are required fields.
                    </Alert>
                </Snackbar>
            </Dialog>
        </div>
    );
}

EditNewCardDialog.defaultProps = {
    title: '',
    description: '',
    deadline: new Date().toLocaleDateString('en-CA'),
    mode: 'add',
};

EditNewCardDialog.propTypes = {
    title: PropTypes.string,
    description: PropTypes.string,
    deadline: PropTypes.string,
    onSaveCard: PropTypes.func.isRequired,
    mode: PropTypes.string,
};

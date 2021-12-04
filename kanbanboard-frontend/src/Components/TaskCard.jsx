import { ListItemText } from '@mui/material';
import React from 'react';
import Card from '@mui/material/Card';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from '@mui/material/IconButton';
import PropTypes from 'prop-types';
import EditNewCardDialog from './EditNewCardDialog';

export default function TaskCard(props) {
    const {
        id,
        title,
        description,
        deadline,
        laneId,
        onRemoveCard,
        onEditCard,
    } = props;

    const deleteCard = () => {
        onRemoveCard(id);
    };

    const editCard = (cardDetails) => {
        onEditCard({
            title: cardDetails.title,
            description: cardDetails.description,
            deadline: cardDetails.deadline,
            id,
            laneId,
        });
    };

    const dateFormatOptions = {
        weekday: 'long',
        year: 'numeric',
        month: 'long',
        day: 'numeric',
    };

    return (
        <Card sx={{ width: 1, padding: 1, maxWidth: 1 }}>
            <ListItemText
                primary={
                    (
                        <Grid
                            container
                            spacing={2}
                            sx={{ width: 1 }}
                            style={{
                                whiteSpace: 'pre-wrap',
                                overflowWrap: 'break-word',
                            }}
                        >
                            <Grid item xs={8}>
                                {title}
                            </Grid>
                            <Grid item xs={2}>
                                <EditNewCardDialog
                                    title={title}
                                    description={description}
                                    deadline={deadline}
                                    onSaveCard={editCard}
                                    mode="edit"
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <IconButton
                                    aria-label="delete"
                                    color="primary"
                                    size="small"
                                    onClick={deleteCard}
                                >
                                    <DeleteIcon fontSize="small" />
                                </IconButton>
                            </Grid>
                        </Grid>
                    )
                }
                secondary={
                    (
                        <>
                            <Typography
                                sx={{ display: 'inline' }}
                                variant="body2"
                                color="text.primary"
                                component="span"
                                style={{ wordWrap: 'break-word' }}
                            >
                                {new Date(deadline).toLocaleDateString(
                                    dateFormatOptions,
                                )}
                            </Typography>
                            <Typography
                                sx={{ display: 'inline', maxWidth: 1 }}
                                variant="body2"
                                component="span"
                                color="text.secondary"
                                style={{ wordWrap: 'break-word' }}
                            >
                                {(description.trim() === '' || description === null) ? '' : ` - ${description}`}
                            </Typography>
                        </>
                    )
                }
            />
        </Card>
    );
}

TaskCard.propTypes = {
    id: PropTypes.number.isRequired,
    title: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired,
    deadline: PropTypes.string.isRequired,
    laneId: PropTypes.number.isRequired,
    onRemoveCard: PropTypes.func.isRequired,
    onEditCard: PropTypes.func.isRequired,
};

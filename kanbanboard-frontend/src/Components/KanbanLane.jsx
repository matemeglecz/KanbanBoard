import React from 'react';
import { Draggable, Droppable } from 'react-beautiful-dnd';
import List from '@mui/material/List';
import Card from '@mui/material/Card';
import ListItem from '@mui/material/ListItem';
import { CardHeader, Divider } from '@mui/material';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import PropTypes from 'prop-types';
import TaskCard from './TaskCard';
import EditNewCardDialog from './EditNewCardDialog';

class KanbanLane extends React.Component {
    constructor(props) {
        super(props);
        Object.assign(this, props);
    }

    renderCard(i) {
        const onRemoveCard = (deletedCardId) => {
            this.onRemoveCard(this.id, deletedCardId);
        };

        const onEditCard = (editedCard) => {
            this.onEditCard(this.id, editedCard);
        };

        return (
            <TaskCard
                id={this.cards[i].id}
                title={this.cards[i].title}
                description={this.cards[i].description}
                laneId={this.cards[i].laneID}
                deadline={this.cards[i].deadline}
                onRemoveCard={onRemoveCard}
                onEditCard={onEditCard}
                oreder={i}
            />
        );
    }

    render() {
        const compareCardOrders = (a, b) => {
            if (a.order < b.order) return -1;
            if (a.order > b.order) return 1;
            return 0;
        };

        this.cards.sort(compareCardOrders);
        const cardsRender = this.cards.map((card, index) => (
            <Draggable
                key={card.id}
                draggableId={card.id.toString()}
                index={index}
            >
                {(provided) => (
                    <ListItem
                        alignitems="flex-start"
                        {...provided.draggableProps}
                        {...provided.dragHandleProps}
                        ref={provided.innerRef}
                    >
                        {this.renderCard(index)}
                    </ListItem>
                )}
            </Draggable>
        ));

        const onAddCard = (newCard) => {
            this.onAddNewCard(this.id, newCard);
        };

        const onRemoveLane = () => {
            this.onRemoveLane(this.id);
        };

        return (
            <Card
                sx={{
                    bgcolor: '#9e9e9e',
                    marginTop: 1,
                    marginLeft: 1,
                    maxWidth: 1,
                }}
            >
                <Grid container direction="row" spacing={0.5}>
                    <Grid item xs={8}>
                        <EditNewCardDialog onSaveCard={onAddCard} mode="new" />
                    </Grid>
                    <Grid item xs={4}>
                        <Button
                            variant="contained"
                            sx={{ width: 1 }}
                            color="error"
                            onClick={onRemoveLane}
                        >
                            Delete
                        </Button>
                    </Grid>
                </Grid>
                <Divider />
                <CardHeader title={this.title} style={{ textAlign: 'center' }} />
                <Divider />
                <Droppable droppableId={this.id.toString()}>
                    {(provided) => (
                        <List
                            alignitems="stretch"
                            {...provided.droppableProps}
                            ref={provided.innerRef}
                        >
                            {cardsRender}
                            {provided.placeholder}
                        </List>
                    )}
                </Droppable>
            </Card>
        );
    }
}

export default KanbanLane;

KanbanLane.propTypes = {
    id: PropTypes.number.isRequired,
    title: PropTypes.string.isRequired,
    cards: PropTypes.arrayOf(PropTypes.shape({
        id: PropTypes.number.isRequired,
        title: PropTypes.string.isRequired,
        description: PropTypes.string.isRequired,
        deadline: PropTypes.string.isRequired,
        laneID: PropTypes.number.isRequired,
    })).isRequired,
    onRemoveCard: PropTypes.func.isRequired,
    onEditCard: PropTypes.func.isRequired,
    onAddNewCard: PropTypes.func.isRequired,
    onRemoveLane: PropTypes.func.isRequired,
};

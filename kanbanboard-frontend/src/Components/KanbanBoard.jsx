import React from 'react';
import { DragDropContext } from 'react-beautiful-dnd';
import Grid from '@mui/material/Grid';
import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';
import Alert from '@mui/material/Alert';
import Snackbar from '@mui/material/Snackbar';
import KanbanLane from './KanbanLane';
import HttpCommunication from '../network/HttpCommunication';
import NewLaneDialog from './NewLaneDialog';

// 'https://61631df3b55edc00175c19a7.mockapi.io/mock/'

class KanbanBoard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            lanes: [],
            isLoading: true,
            isError: false,
        };

        const errorHandler = () => {
            this.setState({ isError: true });
            this.setState({ isLoading: true });
        };

        this.httpCommunication = new HttpCommunication(errorHandler);
    }

    async componentDidMount() {
        const data = await this.httpCommunication.getAll();

        if (await data) {
            await data.sort(this.compareLaneOrders);
            await this.setState({ lanes: data });
            await this.setState({ isLoading: false });
            await this.setState({ isError: false });
        }
    }

    compareLaneOrders = (a, b) => {
        if (a.order < b.order) return -1;
        if (a.order > b.order) return 1;
        return 0;
    };

    renderLane(i) {
        const AddNewCard = async (laneId, card) => {
            const newCard = card;
            const { lanes } = this.state;
            const effectedLane = lanes.find((x) => x.id === laneId);
            const laneIdx = lanes.indexOf(effectedLane);
            lanes.splice(laneIdx, 1);
            newCard.laneID = laneId;
            await this.httpCommunication.postCard(card).then((data) => {
                if (data === undefined) return;
                newCard.id = data.id;
            });

            await effectedLane.cards.push(newCard);
            await lanes.splice(laneIdx, 0, effectedLane);

            await this.setState({ lanes });
        };

        const RemoveCard = (laneId, deletedCardId) => {
            const { lanes } = this.state;
            const effectedLane = lanes.find((x) => x.id === laneId);
            const laneIdx = lanes.indexOf(effectedLane);
            lanes.splice(laneIdx, 1);
            effectedLane.cards.splice(
                effectedLane.cards.indexOf(
                    effectedLane.cards.find((x) => x.id === deletedCardId),
                ),
                1,
            );
            lanes.splice(laneIdx, 0, effectedLane);

            this.setState({ lanes });
            this.httpCommunication.deleteCard(deletedCardId);
        };

        const EditCard = (laneId, editedCard) => {
            const { lanes } = this.state;
            const effectedLane = lanes.find((x) => x.id === laneId);
            const laneIdx = lanes.indexOf(effectedLane);
            lanes.splice(laneIdx, 1);
            const editedCardIdx = effectedLane.cards.indexOf(
                effectedLane.cards.find((x) => x.id === editedCard.id),
            );
            effectedLane.cards.splice(editedCardIdx, 1);
            effectedLane.cards.splice(editedCardIdx, 0, editedCard);
            lanes.splice(laneIdx, 0, effectedLane);

            this.setState({ lanes });
            this.httpCommunication.editCard(editedCard);
        };

        const RemoveLane = (laneId) => {
            const { lanes } = this.state;
            const removedLane = lanes.find((x) => x.id === laneId);
            const removedLaneIdx = lanes.indexOf(removedLane);
            lanes.forEach((l, index) => {
                if (l.order > lanes[removedLaneIdx].order) {
                    lanes[index].order -= 1;
                }
            });
            lanes.splice(removedLaneIdx, 1);

            this.setState({ lanes });
            // console.log(this.state.lanes)
            this.httpCommunication.deleteLane(laneId);
        };

        const { lanes } = this.state;

        const {
            id,
            cards,
            title,
        } = lanes[i];

        return (
            <Grid
                container
                direction="column"
                alignItems="stretch"
                key={id}
                item
                xs={12}
                sm={6}
                md={4}
                lg={2}
                sx={{ maxWidth: 1 }}
            >
                <KanbanLane
                    id={id}
                    cards={cards}
                    title={title}
                    onAddNewCard={AddNewCard}
                    onRemoveCard={RemoveCard}
                    onEditCard={EditCard}
                    onRemoveLane={RemoveLane}
                />
            </Grid>
        );
    }

    render() {
        const { isError, isLoading, lanes } = this.state;

        const renderErrorAlert = () => (
            <Snackbar
                open={isError}
                autoHideDuration={10000}
                anchorOrigin={{ horizontal: 'center', vertical: 'top' }}
            >
                <Alert severity="error" sx={{ width: '100%' }}>
                    There was a problem. Please reload the page.
                </Alert>
            </Snackbar>
        );

        if (isLoading) {
            return (
                <div>
                    <Box
                        sx={{ display: 'flex' }}
                        justifyContent="center"
                        alignItems="center"
                        style={{ minHeight: '90vh' }}
                    >
                        <CircularProgress size={80} />
                    </Box>
                    {renderErrorAlert()}
                </div>
            );
        }

        const handleOnDragEnd = (result) => {
            if (!result.destination) return;
            const items = Array.from(lanes);
            const reorderdItem = items
                .find((x) => x.id === Number(result.source.droppableId))
                .cards.splice(result.source.index, 1);
            items
                .find((x) => x.id === Number(result.destination.droppableId))
                .cards.splice(result.destination.index, 0, reorderdItem[0]);

            this.setState(() => (
                { lanes: items }
            ));

            reorderdItem[0].laneID = Number(result.destination.droppableId);
            reorderdItem[0].order = result.destination.index;
            this.httpCommunication.moveCard(reorderdItem[0]);
        };

        const lanesRender = lanes.map((lane, index) => (
            this.renderLane(index)
        ));

        const onNewLane = async (lane) => {
            const addedLane = lane;
            const newLanes = lanes;
            addedLane.cards = [];
            newLanes.forEach((l, index) => {
                if (l.order >= addedLane.order) {
                    newLanes[index].order += 1;
                }
            });
            await this.httpCommunication.postLane(addedLane).then((data) => {
                if (data === undefined) return;
                addedLane.id = data.id;
            });
            await newLanes.splice(addedLane.order, 0, addedLane);
            await newLanes.sort(this.compareLaneOrders);
            await this.setState({ lanes: newLanes });
        };

        return (
            <div>
                <Grid
                    container
                    spacing={1}
                    justifyContent="left"
                    alignItems="stretch"
                    sx={{ margintop: 1, height: 1, width: 1 }}
                >
                    <DragDropContext onDragEnd={handleOnDragEnd}>
                        {lanesRender}
                    </DragDropContext>
                    <NewLaneDialog onNewLane={onNewLane} />
                </Grid>
                {renderErrorAlert()}
            </div>
        );
    }
}

export default KanbanBoard;

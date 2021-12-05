export default class HttpCommunication {
    host = 'https://localhost:5001';

    URL = 'https://localhost:5001/api';

    tryLimit = 5;

    timeBetweenTriesMs = 500;

    constructor(errorHandler) {
        this.errorHandler = errorHandler;
    }

    async getAll() {
        let response;
        let tries = 0;
        while (response === undefined && tries < this.tryLimit) {
            response = await fetch(`${this.URL}/Lanes`, {
                method: 'GET',
                mode: 'cors',
                withCredentials: true,
                headers: {
                    'Access-Control-Allow-Origin': this.host,
                },
            }).catch(() => {
                this.errorHandler();
            });
            tries += 1;
            if (response === undefined) {
                await setTimeout(null, this.timeBetweenTriesMs);
            }
        }
        return (await typeof response) === 'undefined'
            ? response
            : response.json();
    }

    async postCard(data) {
        const response = await fetch(`${this.URL}/Cards`, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Access-Control-Allow-Origin': this.host,
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        }).catch(() => {
            this.errorHandler();
        });

        return (await typeof response) === 'undefined'
            ? response
            : response.json();
    }

    editCard(data) {
        fetch(`${this.URL}/Cards/${data.id}/edit`, {
            method: 'PUT',
            mode: 'cors',
            headers: {
                'Access-Control-Allow-Origin': this.host,
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        }).catch(() => {
            this.errorHandler();
        });
    }

    moveCard(data) {
        fetch(`${this.URL}/Cards/${data.id}/move`, {
            method: 'PUT',
            mode: 'cors',
            headers: {
                'Access-Control-Allow-Origin': this.host,
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        }).catch(() => {
            this.errorHandler();
        });
    }

    deleteCard(id) {
        fetch(`${this.URL}/Cards/${id}`, {
            method: 'DELETE',
            mode: 'cors',
            headers: {
                'Access-Control-Allow-Origin': this.host,
            },
        })
            .catch(() => {
                this.errorHandler();
            });
    }

    async postLane(data) {
        const response = await fetch(`${this.URL}/Lanes`, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Access-Control-Allow-Origin': this.host,
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        }).catch(() => {
            this.errorHandler();
        });

        return (await typeof response) === 'undefined'
            ? response
            : response.json();
    }

    deleteLane(id) {
        fetch(`${this.URL}/Lanes/${id}`, {
            method: 'DELETE',
            mode: 'cors',
            headers: {
                'Access-Control-Allow-Origin': this.host,
            },
        })
            .catch(() => {
                this.errorHandler();
            });
    }
}

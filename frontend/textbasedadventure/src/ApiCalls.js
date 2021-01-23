const Api = Object.create({}, {
    GetAllPlayers: {
        value: () => {
            return fetch("https://localhost:5001/api/player/getall")
            .then(res => res.json())
        }
    },
    GetAllZones: {
        value: () => {
            return fetch("https://localhost:5001/api/zone/getall")
            .then(res => res.json())
        }
    }
})



export default Api;
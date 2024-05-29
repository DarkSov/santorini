
const express = require('express');
const app = express();
const bodyParser = require('body-parser');
// const cors = require('cors');
const {GameSession} = require('./GameSession.js');


// Set up the server
const PORT = 3000;
// app.use(cors());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

let activeSessions = {};

app.get('/creategame', (req, res) => {
    let session = new GameSession();
    activeSessions[session.redToken] = session;
    activeSessions[session.game.id] = session;
    res.send({redToken: session.redToken, gameId: session.game.id});
});

app.post('/joingame', (req, res) => {
    let gameId = req.body.gameId;
    let session = activeSessions[gameId];
    if(session == null){
        res.send({error: 'Game not found'});
    }
    else if(session.blueToken != null){
        res.send({error: 'Game is full'});
        console.log(session.blueToken);
    }
    else{
        session.initSession();
        activeSessions[session.blueToken] = session;
        res.send({blueToken: session.blueToken});
    }
});

app.post('/maketurn', (req, res) => {
    let x = req.body.x;
    let y = req.body.y;
    let token = req.body.token;
    let session = activeSessions[token];
    if (session == null){
        res.send({error: 'Invalid token'});
        return;
    }
    session.makeTurn(x,y,token);
    res.send({game: session.game});
});

app.get('/getstate', (req, res) => {
    let token = req.query.token;
    let session = activeSessions[token];
    res.send({game: session.game});
});

app.listen(5000, () => {
    console.log(`Server is listening on port 5000`);
});



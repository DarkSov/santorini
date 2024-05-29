
const uid = require('./uid.js');

const GamePhase = {
    BlueSetup: 0,
    RedSetup: 1,
    BlueMove: 2,
    BlueBuild: 3,
    RedMove: 4,
    RedBuild: 5,
    BlueWin: 6,
    RedWin: 7
}


class Game{
    constructor(){
        this.board = Array(5).fill().map(()=>Array(5).fill())
        this.redPlayerX = 0;
        this.redPlayerY = 0;
        this.bluePlayerX = 4;
        this.bluePlayerY = 4;
        this.phase = GamePhase.BlueSetup;
        this.id = uid();
    }

    getGame(id){
        return this.games[id];
    }

    makeTurn(x,y){
        if(this.phase == GamePhase.BlueSetup){
            this.bluePlayerX = x;
            this.bluePlayerY = y;
            this.phase = GamePhase.RedSetup;
        }
        else if(this.phase == GamePhase.RedSetup){
            this.redPlayerX = x;
            this.redPlayerY = y;
            this.phase = GamePhase.BlueMove;
        }
        else if(this.phase == GamePhase.BlueMove){
            this.bluePlayerX = x;
            this.bluePlayerY = y;
            this.phase = GamePhase.BlueBuild;
            if(this.board[x][y] == 3){
                this.phase = GamePhase.BlueWin;
            }
        }
        else if(this.phase == GamePhase.BlueBuild){
            this.board[x][y] += 1;
            this.phase = GamePhase.RedMove;
        }
        else if(this.phase == GamePhase.RedMove){
            this.redPlayerX = x;
            this.redPlayerY = y;
            this.phase = GamePhase.RedBuild;
            if(this.board[x][y] == 3){
                this.phase = GamePhase.RedWin;
            }
        }
        else if(this.phase == GamePhase.RedBuild){
            this.board[x][y] += 1;
            this.phase = GamePhase.BlueMove;
        }
        else if(this.phase == GamePhase.BlueWin){
            return "Blue has already won!";
        }
        else if(this.phase == GamePhase.RedWin){
            return "Red has already won!";
        }
    }
}

module.exports = {Game, GamePhase};


const {Game, GamePhase} = require('./Game.js');
const uid = require('./uid.js');

const timeoutTime = 1000 * 60;

class GameSession{
    constructor(){
        this.redToken = uid(32);
        this.blueToken = null;
        this.game = new Game();
        this.timeoutId = null
    }

    makeTurn(x,y,token){

        if(this.timeoutId != null){
            clearTimeout(this.timeoutId);
        }
        if(token == this.redToken){
            if(this.game.phase == GamePhase.RedMove || this.game.phase == GamePhase.RedSetup){
                this.game.redPlayerX = x;
                this.game.redPlayerY = y;
                this.game.phase = GamePhase.RedBuild;
                if(this.game.board[x][y] == 3){
                    this.game.phase = GamePhase.RedWin;
                }
            }
            else if(this.game.phase == GamePhase.RedBuild){
                this.game.board[x][y] += 1;
                this.game.phase = GamePhase.BlueMove;
            }
        }
        else if(token == this.blueToken){
            if(this.game.phase == GamePhase.BlueMove || this.game.phase == GamePhase.BlueSetup){
                this.game.bluePlayerX = x;
                this.game.bluePlayerY = y;
                this.game.phase = GamePhase.BlueBuild;
                if(this.game.board[x][y] == 3){
                    this.game.phase = GamePhase.BlueWin;
                }
            }
            else if(this.game.phase == GamePhase.BlueBuild){
                this.game.board[x][y] += 1;
                this.game.phase = GamePhase.RedMove;
            }
        }
        if(this.game.phase == GamePhase.BlueMove || this.game.phase == GamePhase.BlueBuild){
            this.timeoutId = setTimeout(()=>{
                this.game.phase = GamePhase.RedWin;
            }, timeoutTime);
        }
        else if(this.game.phase == GamePhase.RedMove || this.game.phase == GamePhase.RedBuild){
            this.timeoutId = setTimeout(()=>{
                this.game.phase = GamePhase.BlueWin;
            }, timeoutTime);
        }
    }
    initSession(){
        this.blueToken = uid(32);
    }
}   

module.exports = {GameSession};


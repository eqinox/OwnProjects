const routes = require('./routes');
const cors = require('cors');
const bodyParser = require('body-parser');
const coockieParser = require('cookie-parser');
const passport = require('passport');

module.exports = (app, settings) => {
    app.use(cors());
    app.use(coockieParser());
    app.use(bodyParser.urlencoded({ extended: false }));
    app.use(bodyParser.json());
    app.use(passport.initialize());
    require('../middlewares/passport')(passport, settings);
    routes(app);
}
const handlers = require('../handlers');
const passport = require('passport');

module.exports = (app) => {
    app.post('/users/login', handlers.user.login);
    app.post('/users/register', handlers.user.register);
    app.post('/users/logout', passport.authenticate('jwt', { session: false }), handlers.user.logout);
    app.get('/authenticated', passport.authenticate('jwt', { session: false }), handlers.user.authenticated);
}
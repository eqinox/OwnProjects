const mongoose = require('mongoose');
const User = mongoose.model('User');
const passportJWT = require('passport-jwt');
const JwtStrategy = passportJWT.Strategy;
const ExtractJwt = passportJWT.ExtractJwt;

module.exports = (passport, settings) => {
    const opts = {
        jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),
        secretOrKey: settings.secret
    };

    passport.use(new JwtStrategy(opts, (payload, next) => {
        User.findById(payload.id).then(res => {
            next(null, res);
        });
    }));
}
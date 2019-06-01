let User = require('mongoose').model('User');
let encryption = require('../utilities/encryption');
const jwt = require('jsonwebtoken');
const logger = require('../utilities/logger');
const environment = process.env.NODE_ENV || 'development';
const settings = require('../config/settings')[environment];

module.exports.register = (req, res) => {
    try {
        const user = {
            username: req.body.username,
            password: req.body.password,
            password2: req.body.password2,
            firstName: req.body.firstName,
            lastName: req.body.lastName,
            email: req.body.email,
            roles: ['User']
        };

        let message = isUserValid(user);
        if (!message.success) {
            return res.status(200).json(message);
        }
        
        let salt = encryption.generateSalt();
        let password = encryption.generateHashedPassword(salt, user.password);

        user.password = password;
        user.salt = salt;
    
        User.create(user)
            .then(user => {
                message = { success: true, message: `Successfuly registered ${user.username}` }
                logger.infoLog.info("User Register: ", message);
                res.status(200).json(message);
            })
            .catch(err => {
                message = { success: false, message: err }
                logger.errorLog.error("Register User Error:", { error: err, body: hidePassword(req.body), headers: req.headers });
                res.status(400).json(message);
            });;
    } catch (err) {
        logger.errorLog.error("Register User Error:", { error: err, body: hidePassword(req.body), headers: req.headers });
        res.status(500).send(err);
    }
}

function isUserValid (user) {
    if (user.username.length < 3 || user.firstName.length < 3) {
        return { success: false, message: 'username, firstName and lastName cannt be less than 3 symbols'}
    }

    if (user.password !== user.password2) {
        return { success: false, message: "passwords doesn't match" };
    }

    return { success: true, message: "user is valid" };
}

// hide the password because dont want people to see the real pass (for logging)
function hidePassword (request) {
    if (request.password && request.password2) {
        request.password = "***",
        request.password2 = "***"
        return request;
    } else {
        return request;
    }
}

module.exports.login = (req, res) => {
    try {
        User.findOne({ username: req.body.username }).then(user => {
            let message = { success: false, message: "Invalid user credentials" }
            if (!user) { return res.status(401).send(message); }
            if (!user.authenticate(req.body.password)) { return res.status(401).send(message); }
    
            const payload = { id: user.id };
            const token = jwt.sign(payload, settings.secret);
            message = { success: true, message: `Successfuly logged in ${user.username}`, user: { username: user.username, token}}
            logger.tempLog.info("User Login: ", message);
            res.status(200).send(message);
        });
    } catch (err) {
        res.status(500).send(err);
    }
}

module.exports.logout = (req, res) => {
    if (req.isAuthenticated()) {
        logger.tempLog.info("Logout User:", { message: `Succesfuly logged our ${req.user.username}`})
        res.status(200).json({ success: true, message: `Succesfuly logged out ${req.user.username}` });
        req.logout();
    } else {
        res.status(401).json({ success: false, message: "User not found" });
    }
}

module.exports.authenticated = (req, res) => {
    res.status(200).json({ success: true, message: "Welcome " + req.user.username });
}

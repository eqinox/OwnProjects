const { createLogger, format, transports } = require('winston');
const path = require('path');

const error = createLogger({
    level: 'error',
    format: format.combine(
      format.timestamp({format: 'YYYY-MM-DD HH:mm:ss'}),
      format.label({ label: path.basename(process.mainModule.filename) }),
      format.printf(error => `${error.timestamp} ${error.level} [${error.label}]:: ${error.message} ${error.error} Body: ${JSON.stringify(error.body)} Headers: ${JSON.stringify(error.headers)}`)
    ), 
    defaultMeta: { service: 'user-service' },
    transports: [
        new transports.File({ filename: './logs/error-log' }),
        new transports.Console({ 
          format: format.combine(
            format.colorize(),
            format.printf(error => `${error.timestamp} ${error.level} [${error.label}]:: ${error.message} ${error.error} Body: ${JSON.stringify(error.body)} Headers: ${JSON.stringify(error.headers)}`) // add this because want colorize working
          )}),
    ]
  });

const info = createLogger({
  level: 'info',
  format: format.combine(
    format.timestamp({format: 'YYYY-MM-DD HH:mm:ss'}),
    format.label({ label: path.basename(process.mainModule.filename) }),    
    format.printf(info => `${info.timestamp} ${info.level} [${info.label}]:: ${info.message}`)
  ),
  defaultMeta: { service: 'user-service' },
  transports: [
      new transports.File({ filename: './logs/log' }),
      new transports.Console({ 
        format: format.combine(
          format.colorize(), 
          format.printf(info => `${info.timestamp} ${info.level} [${info.label}]:: ${info.message}`) // add this because want colorize working
        )}),
  ]
});

const temp = createLogger({
  level: 'info',
  format: format.combine(
    format.timestamp({format: 'YYYY-MM-DD HH:mm:ss'}),
    format.label({ label: path.basename(process.mainModule.filename) }),    
    format.printf(info => `${info.timestamp} ${info.level} [${info.label}]:: ${info.message}`)
  ), 
  defaultMeta: { service: 'user-service' },
  transports: [
      new transports.File({ filename: './logs/temp-log' }),
      new transports.Console({ 
        format: format.combine(
          format.colorize(), 
          format.printf(info => `${info.timestamp} ${info.level} [${info.label}]:: ${info.message}`) // add this because want colorize working
        )}),
  ]
});

module.exports = {
  errorLog: error,
  infoLog: info,
  tempLog: temp
}
#################
# Build the app #
#################
FROM node:12-alpine as build

WORKDIR /app

COPY package.json package-lock.json ./

RUN npm install
COPY . .

RUN npm install -g @angular/cli
RUN ng build --configuration production --output-path=/dist

################
# Run in NGINX #
################
FROM nginx:alpine

COPY --from=build /dist /usr/share/nginx/html
COPY ./nginx.conf /etc/nginx/conf.d/default.conf

# COPY --from=build /dist /usr/src/app/public

# # When the container starts, replace the env.js with values from environment variables
CMD ["/bin/sh",  "-c",  "envsubst < /usr/share/nginx/html/assets/env.template.js > /usr/share/nginx/html/assets/env.js && exec nginx -g 'daemon off;'"]


# COPY --from=build /dist /usr/src/app/

# When the container starts, replace the env.js with values from environment variables
# CMD ["/bin/sh",  "-c",  "envsubst < /dist/engenharia-app/assets/env.template.js > /dist/engenharia-app/assets/env.js"]
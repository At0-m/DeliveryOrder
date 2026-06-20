FROM node:22-alpine AS build

WORKDIR /app

ARG NPM_REGISTRY=https://registry.npmjs.org/

COPY frontend/package*.json frontend/.npmrc ./

RUN npm config set registry $NPM_REGISTRY && npm install

COPY frontend ./

RUN npm run build

FROM nginx:1.27-alpine

COPY frontend/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80
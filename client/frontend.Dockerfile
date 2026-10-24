# syntax=docker/dockerfile:1

FROM node:18-alpine AS base

WORKDIR /app

FROM base AS deps

RUN --mount=type=bind,source=package.json,target=package.json \
    --mount=type=bind,source=package-lock.json,target=package-lock.json \
    --mount=type=bind,source=jsconfig.json,target=jsconfig.json \
    --mount=type=cache,target=/root/.npm \
    npm ci --omit=dev

FROM deps AS build

RUN --mount=type=bind,source=package.json,target=package.json \
    --mount=type=bind,source=package-lock.json,target=package-lock.json \
    --mount=type=bind,source=jsconfig.json,target=jsconfig.json \
    --mount=type=cache,target=/root/.npm \
    npm ci

COPY . .

RUN npm run build

FROM base AS final

WORKDIR /app

ENV NODE_ENV=production

COPY package.json .
COPY jsconfig.json .

COPY --from=deps app/node_modules ./node_modules

COPY --from=build app/build ./build

COPY ./src ./src
COPY ./public ./public

RUN mkdir -p app/node_modules/.cache && \
    chown -R node:node app/node_modules/.cache

EXPOSE 3000

CMD ["npm", "start"]

# pull official base image
FROM node:14.18.1

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY package.json ./
RUN yarn && yarn cache clean

# add app
COPY . ./

# start app
CMD ["yarn", "start"]
# Derived from https://hub.docker.com/r/petervaczi/antlr/

FROM anapsix/alpine-java:8
MAINTAINER Jakub Linhart <jakub.linhart@gmail.com>

ENV ANTLR_VERSION 4.7
ENV CLASSPATH .:/antlr-${ANTLR_VERSION}-complete.jar:$CLASSPATH

ADD http://www.antlr.org/download/antlr-${ANTLR_VERSION}-complete.jar /
RUN chmod +r /antlr-${ANTLR_VERSION}-complete.jar

COPY rootfs/ /

ENTRYPOINT ["/usr/bin/antlr"]

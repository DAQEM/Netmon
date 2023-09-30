FROM polinux/snmpd:alpine
WORKDIR /

RUN apk update
RUN apk add curl

COPY ["snmpd.conf", "./etc/snmp/"]
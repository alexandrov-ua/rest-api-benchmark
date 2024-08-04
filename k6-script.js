import http from 'k6/http';
import { sleep } from 'k6';
export const options = {
  vus: 200,
  duration: '1m',
  tags: {
    name: 'url',
  },
};
export default function () {
  var baseUrl = `${__ENV.API_URL}`
  http.get(baseUrl+'/api/v1/authors');
  var resp = http.post(baseUrl+'/api/v1/authors',JSON.stringify({
    name:"qwe1",
    bio: "asdfdsf"
  }),{
    headers: { 'Content-Type': 'application/json' },
  });
  var createdId = resp.json().id

  http.get(baseUrl+'/api/v1/authors/'+createdId);
  http.del(baseUrl+'/api/v1/authors/'+createdId);
  http.get(baseUrl+'/api/v1/authors');
}
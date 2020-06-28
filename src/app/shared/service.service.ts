import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  constructor(private http: HttpClient) { }

  getContactList() {
    return this.http.get(environment.apiBaseURI + '/contact');
  }
  postContact(formData) {
    return this.http.post(environment.apiBaseURI + '/contact', formData);
  }

  putContact(formData) {
    return this.http.put(environment.apiBaseURI + '/contact/' + formData.contactID, formData);
  }

  deleteContactt(id) {
    return this.http.delete(environment.apiBaseURI + '/contact/' + id);
  }

  getContact() {
    return this.http.get(environment.apiBaseURI + '/contact');
  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormArray, Validators, FormGroup } from '@angular/forms';
import { ServiceService } from '../shared/service.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  contactForms: FormArray = this.fb.array([]);
  contactList = [];
  notification = null;

  constructor(private fb: FormBuilder,
    private contactService:ServiceService) { }

  ngOnInit() {
    
    this.contactService.getContactList()
      .subscribe(res => this.contactList = res as []);

    this.contactService.getContact().subscribe(
      res => {
        if (res == [])
          this.addContactForm();
        else {
          //generate formarray as per the data received from Contact  table
          (res as []).forEach((contact: any) => {
            this.contactForms.push(this.fb.group({
              contactID: [contact.contactID],
              name: [contact.name, Validators.required],
              phoneNumber: [contact.phoneNumber, Validators.required],
            }));
          });
        }
      }
    );
  }

  addContactForm() {
    this.contactForms.push(this.fb.group({
      contactID: [0],
      name: ['', Validators.required],
      phoneNumber: ['', Validators.required],
    }));
  }

  recordSubmit(fg: FormGroup) {
    if (fg.value.contactID == 0)
      this.contactService.postContact(fg.value).subscribe(
        (res: any) => {
          fg.patchValue({ contactID: res.contactID });
          this.showNotification('insert');
        });
    else
      this.contactService.putContact(fg.value).subscribe(
        (res: any) => {
          this.showNotification('update');
        });
  }

  onDelete(contactID, i) {
    if (contactID == 0)
      this.contactForms.removeAt(i);
    else if (confirm('Are you sure to delete this record ?'))
      this.contactService.deleteContactt(contactID).subscribe(
        res => {
          this.contactForms.removeAt(i);
          this.showNotification('delete');
        });
  }

  showNotification(category) {
    switch (category) {
      case 'insert':
        this.notification = { class: 'text-success', message: 'saved!' };
        break;
      case 'update':
        this.notification = { class: 'text-primary', message: 'updated!' };
        break;
      case 'delete':
        this.notification = { class: 'text-danger', message: 'deleted!' };
        break;

      default:
        break;
    }
    setTimeout(() => {
      this.notification = null;
    }, 3000);
  }

}


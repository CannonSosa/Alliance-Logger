import { TestBed } from '@angular/core/testing';

import { PostLogsService } from './post-logs.service';

describe('PostLogsService', () => {
  let service: PostLogsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PostLogsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
